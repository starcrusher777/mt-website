import type { NextApiRequest, NextApiResponse } from 'next';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';

export const config = {
    api: { bodyParser: false },
};

export default async function handler(
    req: NextApiRequest,
    res: NextApiResponse
) {
    if (req.method !== 'POST') {
        res.setHeader('Allow', 'POST');
        return res.status(405).json({ error: 'Method not allowed' });
    }

    try {
        const chunks: Buffer[] = [];
        for await (const chunk of req as any) {
            chunks.push(Buffer.isBuffer(chunk) ? chunk : Buffer.from(chunk));
        }
        const body = Buffer.concat(chunks);
        const contentType = req.headers['content-type'] || '';

        const response = await fetch(`${API_URL}/api/v1/Order/CreateOrder`, {
            method: 'POST',
            headers: contentType ? { 'Content-Type': contentType } : undefined,
            body,
        });

        if (!response.ok) {
            const text = await response.text();
            return res.status(response.status).send(text);
        }
        const body = await response.json().catch(() => ({}));
        const data = body.data ?? body;
        res.status(200).json(data);
    } catch (err) {
        console.error('Order create proxy error:', err);
        res.status(502).json({ error: 'Failed to reach API.' });
    }
}

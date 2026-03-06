import type { NextApiRequest, NextApiResponse } from 'next';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';
const API_V1 = `${API_URL}/api/v1`;

export const config = {
    api: { bodyParser: false },
};

export default async function handler(
    req: NextApiRequest,
    res: NextApiResponse
) {
    const { id } = req.query;
    if (!id || Array.isArray(id)) {
        return res.status(400).json({ error: 'Order id required' });
    }

    if (req.method === 'GET') {
        try {
            const response = await fetch(`${API_V1}/Order/GetOrder/${id}`);
            if (!response.ok) {
                const errBody = await response.json().catch(() => ({}));
                return res.status(response.status).json({ error: errBody.errors?.[0]?.message ?? 'Failed to get order' });
            }
            const body = await response.json();
            const data = body.data ?? body;
            return res.status(200).json(data);
        } catch (err) {
            console.error('Order get proxy error:', err);
            return res.status(502).json({ error: 'Failed to reach API.' });
        }
    }

    if (req.method === 'PUT') {
        try {
            const chunks: Buffer[] = [];
            for await (const chunk of req as any) {
                chunks.push(Buffer.isBuffer(chunk) ? chunk : Buffer.from(chunk));
            }
            const body = Buffer.concat(chunks);
            const contentType = req.headers['content-type'] || '';

            const response = await fetch(`${API_V1}/Order/UpdateOrder/${id}`, {
                method: 'PUT',
                headers: contentType ? { 'Content-Type': contentType } : undefined,
                body,
            });
            if (!response.ok) {
                const text = await response.text();
                return res.status(response.status).send(text);
            }
            const body = await response.json().catch(() => ({}));
            const data = body.data ?? body;
            return res.status(200).json(data);
        } catch (err) {
            console.error('Order update proxy error:', err);
            return res.status(502).json({ error: 'Failed to reach API.' });
        }
    }

    res.setHeader('Allow', 'GET, PUT');
    return res.status(405).json({ error: 'Method not allowed' });
}

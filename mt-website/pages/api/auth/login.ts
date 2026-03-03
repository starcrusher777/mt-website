import type { NextApiRequest, NextApiResponse } from 'next';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';

export default async function handler(
    req: NextApiRequest,
    res: NextApiResponse
) {
    if (req.method !== 'POST') {
        res.setHeader('Allow', 'POST');
        return res.status(405).json({ error: 'Method not allowed' });
    }

    try {
        const response = await fetch(`${API_URL}/api/Auth/Login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(req.body),
        });
        const data = await response.json().catch(() => ({}));
        if (!response.ok) {
            return res.status(response.status).json(data);
        }
        res.status(200).json(data);
    } catch (err) {
        console.error('Auth login proxy error:', err);
        res.status(502).json({ error: 'Failed to reach API.' });
    }
}

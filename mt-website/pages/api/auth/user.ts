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

    const auth = req.headers.authorization;
    if (!auth) {
        return res.status(401).json({ error: 'Authorization required' });
    }

    try {
        const response = await fetch(`${API_URL}/api/Auth/GetUser`, {
            method: 'POST',
            headers: {
                'Content-Type': 'text/plain',
                'Authorization': auth,
            },
        });
        if (!response.ok) {
            return res.status(response.status).json({ error: 'Failed to get user' });
        }
        const data = await response.json();
        res.status(200).json(data);
    } catch (err) {
        console.error('Auth get user proxy error:', err);
        res.status(502).json({ error: 'Failed to reach API.' });
    }
}

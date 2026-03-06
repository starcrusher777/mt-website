import type { NextApiRequest, NextApiResponse } from 'next';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';

export default async function handler(
    req: NextApiRequest,
    res: NextApiResponse
) {
    const { id } = req.query;
    if (!id || Array.isArray(id)) {
        return res.status(400).json({ error: 'User id required' });
    }

    if (req.method === 'GET') {
        try {
            const response = await fetch(
                `${API_URL}/api/v1/User/GetUser/${id}`
            );
            if (!response.ok) {
                const errBody = await response.json().catch(() => ({}));
                return res.status(response.status).json({ error: errBody.errors?.[0]?.message ?? 'Failed to get user' });
            }
            const body = await response.json();
            const data = body.data ?? body;
            return res.status(200).json(data);
        } catch (err) {
            console.error('User get proxy error:', err);
            return res.status(502).json({ error: 'Failed to reach API.' });
        }
    }

    if (req.method === 'PUT') {
        const auth = req.headers.authorization;
        if (!auth) {
            return res.status(401).json({ error: 'Authorization required' });
        }
        try {
            const response = await fetch(
                `${API_URL}/api/v1/User/UpdateUser/${id}`,
                {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': auth,
                    },
                    body: JSON.stringify(req.body),
                }
            );
            if (!response.ok) {
                return res.status(response.status).json({ error: 'Update failed' });
            }
            const body = await response.json().catch(() => ({}));
            const data = body.data ?? body;
            return res.status(200).json(data);
        } catch (err) {
            console.error('User update proxy error:', err);
            return res.status(502).json({ error: 'Failed to reach API.' });
        }
    }

    res.setHeader('Allow', 'GET, PUT');
    return res.status(405).json({ error: 'Method not allowed' });
}

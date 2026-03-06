import { useState, useEffect } from 'react';
import { useRouter } from 'next/router';
import Link from 'next/link';

export default function CreateNew() {
    const router = useRouter();
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');
    const [images, setImages] = useState<FileList | null>(null);
    const [loading, setLoading] = useState(false);
    const [authChecked, setAuthChecked] = useState(false);

    useEffect(() => {
        const userId = localStorage.getItem('userid') ?? localStorage.getItem('userId');
        if (!userId) {
            router.replace('/auth');
            return;
        }
        setAuthChecked(true);
    }, [router]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const userId = localStorage.getItem('userid') ?? localStorage.getItem('userId');
        if (!userId) {
            alert('Please log in to create a listing.');
            router.push('/auth');
            return;
        }

        const formData = new FormData();
        formData.append('OrderName', name);
        formData.append('Type', '0'); // Sell
        formData.append('Status', '0'); // Active
        formData.append('Item.Name', name);
        formData.append('Item.Description', description);
        formData.append('Item.Price', price);
        formData.append('Item.Quantity', '1');
        formData.append('UserId', userId);

        if (images && images.length > 0) {
            Array.from(images).forEach((file) => {
                formData.append('Images', file);
            });
        }

        setLoading(true);
        try {
            const response = await fetch('/api/order/create', {
                method: 'POST',
                body: formData,
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || 'Server error');
            }

            const result = await response.json();
            console.log('Listing created:', result);
            await router.push('/');
        } catch (error: unknown) {
            console.error('Error:', error);
            const message = error instanceof Error ? error.message : 'Failed to create listing';
            alert(`Error creating listing: ${message}`);
        } finally {
            setLoading(false);
        }
    };

    if (!authChecked) {
        return (
            <div className="form-container">
                <p className="form-title">Checking login...</p>
            </div>
        );
    }

    return (
        <div className="form-container">
            <h1 className="form-title">Create listing</h1>
            <form onSubmit={handleSubmit} className="form" encType="multipart/form-data">
                <input
                    type="text"
                    placeholder="Title"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    className="form-input"
                    required
                />
                <textarea
                    placeholder="Description"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    className="form-textarea"
                    required
                />
                <input
                    type="number"
                    placeholder="Price"
                    min="0"
                    step="any"
                    value={price}
                    onChange={(e) => setPrice(e.target.value)}
                    className="form-input"
                    required
                />
                <label className="form-label">
                    Product photos (multiple allowed)
                    <input
                        type="file"
                        multiple
                        accept="image/*"
                        onChange={(e) => setImages(e.target.files)}
                        className="form-input"
                        required
                    />
                </label>
                <div className="form-actions">
                    <button type="submit" className="form-button" disabled={loading}>
                        {loading ? 'Submitting...' : 'Create listing'}
                    </button>
                    <Link href="/" className="form-link">Cancel</Link>
                </div>
            </form>
        </div>
    );
}

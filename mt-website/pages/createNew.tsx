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
            alert('Войдите в аккаунт, чтобы разместить объявление.');
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
                throw new Error(errorText || 'Ошибка сервера');
            }

            const result = await response.json();
            console.log('Объявление создано:', result);
            await router.push('/');
        } catch (error: unknown) {
            console.error('Ошибка:', error);
            const message = error instanceof Error ? error.message : 'Не удалось создать объявление';
            alert(`Ошибка при создании объявления: ${message}`);
        } finally {
            setLoading(false);
        }
    };

    if (!authChecked) {
        return (
            <div className="form-container">
                <p className="form-title">Проверка входа...</p>
            </div>
        );
    }

    return (
        <div className="form-container">
            <h1 className="form-title">Создать объявление</h1>
            <form onSubmit={handleSubmit} className="form" encType="multipart/form-data">
                <input
                    type="text"
                    placeholder="Название"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    className="form-input"
                    required
                />
                <textarea
                    placeholder="Описание"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    className="form-textarea"
                    required
                />
                <input
                    type="number"
                    placeholder="Цена (₽)"
                    min="0"
                    step="any"
                    value={price}
                    onChange={(e) => setPrice(e.target.value)}
                    className="form-input"
                    required
                />
                <label className="form-label">
                    Фото товара (можно несколько)
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
                        {loading ? 'Отправка...' : 'Создать объявление'}
                    </button>
                    <Link href="/" className="form-link">Отмена</Link>
                </div>
            </form>
        </div>
    );
}

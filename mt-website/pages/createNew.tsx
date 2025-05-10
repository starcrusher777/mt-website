import { useState } from 'react';
import { useRouter } from 'next/router';

export default function CreateNew() {
    const router = useRouter();
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');
    const [images, setImages] = useState<FileList | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const userId = localStorage.getItem('userid');
        
        const formData = new FormData();
        formData.append('OrderName', name);
        formData.append('Type', 'Sell');
        formData.append('Status', '0');
        formData.append('Item.Name', name);
        formData.append('Item.Description', description);
        formData.append('Item.Price', price);
        formData.append('Item.Quantity', '1');
        formData.append('UserId', userId || '');

        if (images) {
            Array.from(images).forEach((file) => {
                formData.append('Images', file);
            });
        }

        try {
            const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Order/CreateOrder`, {
                method: 'POST',
                body: formData,
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText);
            }

            const result = await response.json();
            console.log('Объявление создано:', result);
            router.push('/');
        } catch (error: any) {
            console.error('Ошибка:', error);
            alert(`Ошибка при создании объявления: ${error.message}`);
        }
    };

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
                    placeholder="Цена"
                    value={price}
                    onChange={(e) => setPrice(e.target.value)}
                    className="form-input"
                    required
                />
                <input
                    type="file"
                    multiple
                    accept="image/*"
                    onChange={(e) => setImages(e.target.files)}
                    className="form-input"
                    required
                />
                <button type="submit" className="form-button">
                    Создать
                </button>
            </form>
        </div>
    );
}
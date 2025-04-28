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

        const formData = new FormData();
        formData.append('OrderName', name);
        formData.append('Type', 'Sell');
        formData.append('Status', '0');
        formData.append('Item.Name', name);
        formData.append('Item.Description', description);
        formData.append('Item.Price', price);
        formData.append('Item.Quantity', '1');
        
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
        <div className="max-w-xl mx-auto p-6">
            <h1 className="text-2xl font-bold mb-4">Создать объявление</h1>
            <form onSubmit={handleSubmit} className="space-y-4" encType="multipart/form-data">
                <input
                    type="text"
                    placeholder="Название"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    className="w-full border px-4 py-2 rounded"
                    required
                />
                <textarea
                    placeholder="Описание"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    className="w-full border px-4 py-2 rounded"
                    required
                />
                <input
                    type="number"
                    placeholder="Цена"
                    value={price}
                    onChange={(e) => setPrice(e.target.value)}
                    className="w-full border px-4 py-2 rounded"
                    required
                />
                <input
                    type="file"
                    multiple
                    accept="image/*"
                    onChange={(e) => setImages(e.target.files)}
                    className="w-full border px-4 py-2 rounded"
                    required
                />
                <button
                    type="submit"
                    className="bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700"
                >
                    Создать
                </button>
            </form>
        </div>
    );
}

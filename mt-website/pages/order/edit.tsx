import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import styles from '../../styles/UserProfile.module.css';

interface Item {
    name: string;
    description: string;
    price: number;
    quantity: number;
}

interface Order {
    id: number;
    orderId: number;
    orderName: string;
    status: number;
    type: number;
    item: Item;
    itemImages: string[]; // For preview
}

export default function EditOrderPage() {
    const router = useRouter();
    const {id} = router.query;
    const [order, setOrder] = useState<Order | null>(null);
    const [images, setImages] = useState<File[]>([]);

    useEffect(() => {
        if (!id) return;
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Order/GetOrder/${id}`)
            .then(res => res.json())
            .then(data => {
                setOrder(data);
            });
    }, [id]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        if (!order) return;
        const {name, value} = e.target;

        if (name in order.item) {
            setOrder({...order, item: {...order.item, [name]: value}});
        } else {
            setOrder({...order, [name]: value});
        }
    };

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files) {
            setImages(Array.from(e.target.files));
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!order) return;

        const formData = new FormData();
        formData.append('OrderName', order.orderName);
        formData.append('Status', order.status.toString());
        formData.append('Type', order.type.toString());

        formData.append('Item.Name', order.item.name);
        formData.append('Item.Description', order.item.description);
        formData.append('Item.Price', order.item.price.toString());
        formData.append('Item.Quantity', order.item.quantity.toString());

        images.forEach((image, index) => {
            formData.append(`Images`, image);
        });

        const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Order/UpdateOrder/${id}`, {
            method: 'PUT',
            body: formData,
        });

        if (response.ok) {
            alert('Объявление обновлено');
            router.push(`/order/${id}`);
        } else {
            alert('Ошибка при обновлении');
        }
    };

    if (!order) return <div>Загрузка...</div>;

    return (
        <div className={styles.page}>
            <h1 className={styles.title}>Редактировать объявление</h1>
            <form onSubmit={handleSubmit} className="form" encType="multipart/form-data">
                <input name="orderName" value={order.orderName} onChange={handleChange} className="form-input" placeholder="Название"/>
                <input name="status" value={order.status} onChange={handleChange} className="form-input" placeholder="Статус (0 или 1)"/>
                <input name="type" value={order.type} onChange={handleChange} className="form-input" placeholder="Тип (0 или 1)"/>

                <input name="name" value={order.item.name} onChange={handleChange} className="form-input" placeholder="Имя товара"/>
                <textarea name="description" value={order.item.description} onChange={handleChange} className="form-input"
                          placeholder="Описание"/>
                <input type="number" name="price" value={order.item.price} onChange={handleChange} className="form-input" placeholder="Цена"/>
                <input type="number" name="quantity" value={order.item.quantity} onChange={handleChange} className="form-input"
                       placeholder="Количество"/>

                <input type="file" className="anime-button" multiple onChange={handleFileChange}/>
                <button type="submit" className="anime-button">Сохранить</button>
            </form>
        </div>
    );
}
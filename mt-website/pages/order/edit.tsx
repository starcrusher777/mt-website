import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import styles from '../../styles/UserProfile.module.css';

interface Order {
    id: number;
    orderId: number;
    orderName: string;
    status: number;
    type: number;
    item: {
        name: string;
        description: string;
        price: number;
        quantity: number;
        images: { id: number; imageUrl: string}[];
    }
}

export default function EditOrderPage() {
    const router = useRouter();
    const {id} = router.query;
    const apiBase = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';
    const [order, setOrder] = useState<Order | null>(null);
    const [images, setImages] = useState<File[]>([]);
    const [imagesToDelete, setImagesToDelete] = useState<number[]>([]);

    useEffect(() => {
        if (!id) return;
        fetch(`/api/order/${id}`)
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

    const handleRemoveOldImage = (imageId: number) => {
        setOrder(prev => {
            if (!prev) return prev;
            return {
                ...prev,
                itemImages: prev.item.images.filter(img => img.id !== imageId),
            };
        });
        setImagesToDelete(prev => [...prev, imageId]);
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
        imagesToDelete.forEach(id => {
            formData.append('ImagesToDelete', id.toString());
        });

        const response = await 
            fetch(`/api/order/${id}`, {
            method: 'PUT',
            body: formData,
        });

        if (response.ok) {
            alert('Listing updated');
            await router.push(`/order/${id}`);
        } else {
            alert('Update failed');
        }
    };

    if (!order) return <div>Loading...</div>;

    return (
        <div className={styles.page}>
            <h1 className={styles.title}>Edit listing</h1>
            <form onSubmit={handleSubmit} className="form" encType="multipart/form-data">
                <input name="orderName" value={order.orderName} onChange={handleChange}
                       className="form-input" placeholder="Title"/>
                <input name="status" value={order.status} onChange={handleChange}
                       className="form-input" placeholder="Status (0 or 1)"/>
                <input name="type" value={order.type} onChange={handleChange}
                       className="form-input" placeholder="Type (0 or 1)"/>

                <input name="name" value={order.item.name} onChange={handleChange}
                       className="form-input" placeholder="Item name"/>
                <textarea name="description" value={order.item.description} onChange={handleChange}
                          className="form-input"
                          placeholder="Description"/>
                <input type="number" name="price" value={order.item.price} onChange={handleChange}
                       className="form-input" placeholder="Price"/>
                <input type="number" name="quantity" value={order.item.quantity} onChange={handleChange}
                       className="form-input"
                       placeholder="Quantity"/>
                <input type="file" className="anime-button" multiple onChange={handleFileChange}/>
            </form>
            <div className={styles.imageGallery}>
                {order.item.images.map((img, index) => (
                    <div key={img.id} className={styles.imageWrapper}>
                        <img src={`${apiBase}${img.imageUrl}`} alt={`Image ${index + 1}`}
                             className={styles.thumbnail}/>
                        <button
                            type="button"
                            className={styles.deleteButton}
                            onClick={() => handleRemoveOldImage(img.id)}
                        >
                            ✖
                        </button>
                    </div>
                ))}
            </div>
            <div className={styles.page}>
                <button type="submit" className="anime-button">Save</button>
            </div>
        </div>
    );
}
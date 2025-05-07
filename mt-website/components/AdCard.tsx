interface AdCardProps {
    title: string;
    description?: string;
    price: number;
    imageUrl?: string;
}

export default function AdCard({ title, description, price, imageUrl }: AdCardProps) {
    return (
        <div className="ad-card">
            <img
                src={imageUrl || '/placeholder.jpg'}
                alt={title}
                className="ad-card-image"
            />
            <div className="ad-card-content">
                <h2 className="ad-card-title">{title}</h2>
                {description && <p className="ad-card-description">{description}</p>}
                <p className="ad-card-price">{price} ₽</p>
            </div>
        </div>
    );
}

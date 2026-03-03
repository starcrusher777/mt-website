import React from 'react';

interface StatCardProps {
    label: string;
    value: number | string;
    progress?: number; // 0-100 for progress bar
    icon?: React.ReactNode;
}

const StatCard: React.FC<StatCardProps> = ({ label, value, progress, icon }) => {
    return (
        <div className="stat-card">
            {icon && <div className="stat-icon">{icon}</div>}
            <div className="stat-content">
                <div className="stat-value">{value}</div>
                <div className="stat-label">{label}</div>
                {progress !== undefined && (
                    <div className="stat-progress">
                        <div
                            className="stat-progress-bar"
                            style={{ width: `${progress}%` }}
                        />
                    </div>
                )}
            </div>
        </div>
    );
};

export default StatCard;


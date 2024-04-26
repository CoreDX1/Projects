export interface Tasks {
    title: string;
    description: string;
    completed: boolean;
}

export type FilterType = 'all' | 'active' | 'completed';

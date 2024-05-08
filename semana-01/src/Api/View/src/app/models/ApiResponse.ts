export interface ApiResponse<T> {
    meta: {
        statusCode: number;
        message: string;
    };
    data: T;
    errors: ErrorLogin;
}

interface User {
    userId: number;
    userName: string;
    password: string;
    email: string;
    createAt: string;
}

interface ErrorLogin {
    Password: Array<string>;
    Email: Array<string>;
}

interface Task {
    id: number;
    title: string;
    description: string;
    completed: boolean;
}

export interface Data {
    user: User;
    lists: Task[];
}

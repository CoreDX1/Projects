export interface ApiResponse<T> {
    data: T;
    IsSuccess: boolean;
    statuCode: number;
    message: string;
}

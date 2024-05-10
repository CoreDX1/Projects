import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Tasks } from '../models/todo';
import { AccountLoginRequest } from '../models/AccountLoginRequest';
import { Observable } from 'rxjs';
import { ApiResult, Data } from '../models/ApiResult';

@Injectable({
    providedIn: 'root',
})
export class TasksService {
    private http = inject(HttpClient);
    private urlApi = 'http://localhost:5100/api/Account';

    PostTask(account: AccountLoginRequest): Observable<ApiResult<Data>> {
        const task = this.http.post<ApiResult<Data>>(`${this.urlApi}/login`, account);
        return task;
    }

    public DeleteTask(id: number): Observable<Tasks> {
        const optios = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            }),
            body: {
                id: id,
            },
        };
        const task = this.http.delete<Tasks>('http://localhost:5100/api/Account/DeleteTask', optios);

        return task;
    }
}

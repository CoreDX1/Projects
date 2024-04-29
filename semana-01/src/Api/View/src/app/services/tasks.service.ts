import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Tasks } from '../models/todo';
import { AccountLoginRequest } from '../models/AccountLoginRequest';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class TasksService {
    private http = inject(HttpClient);
    private urlApi = 'http://localhost:5100/api/Account';

    public PostTask(account: AccountLoginRequest): Observable<Tasks[]> {
        let task = this.http.post<Tasks[]>(`${this.urlApi}/GetTasks`, account);
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
        const task = this.http.delete<Tasks>(
            'http://localhost:5100/api/Account/DeleteTask',
            optios
        );

        return task;
    }
}

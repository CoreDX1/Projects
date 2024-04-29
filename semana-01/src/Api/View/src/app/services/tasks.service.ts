import { HttpClient, HttpResponse } from '@angular/common/http';
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
}

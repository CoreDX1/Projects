import { Component, OnInit, inject } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { AccountLoginRequest } from '../../models/AccountLoginRequest';
import { Tasks } from '../../models/todo';
import { catchError } from 'rxjs';
import { LoginComponent } from '../login/login.component';

@Component({
    selector: 'app-todo',
    standalone: true,
    imports: [LoginComponent],
    templateUrl: './todo.component.html',
})
export class TodoComponent implements OnInit {
    private taskService = inject(TasksService);

    public listTasks: Tasks[] = [];

    public userDetails = {
        email: '',
        password: '',
    };

    public isLogged: boolean = false;

    public account: AccountLoginRequest = {
        password: 'password123',
        email: 'johndoe@example.com',
    };

    ngOnInit(): void {
        this.getTasks();
        this.submitForm();
    }

    public getTasks() {
        const tasks = this.taskService
            .PostTask(this.account)
            .subscribe(task => {
                this.listTasks = task;
            });
        return tasks;
    }

    public DeleteTask(id: number) {
        return this.taskService.DeleteTask(id).subscribe(() => {
            this.getTasks();
        });
    }

    public submitForm() {
        console.log('Form data:', this.userDetails);
    }
}

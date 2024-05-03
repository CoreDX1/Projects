import { Component, OnInit, inject } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { AccountLoginRequest } from '../../models/AccountLoginRequest';
import { Tasks } from '../../models/todo';
import { LoginComponent } from '../login/login.component';
import { ApiResponse } from '../../models/ApiResponse';

@Component({
    selector: 'app-todo',
    standalone: true,
    imports: [LoginComponent],
    templateUrl: './todo.component.html',
})
export class TodoComponent implements OnInit {
    private taskService = inject(TasksService);

    public listTasks: ApiResponse<Array<Tasks>> = {
        data: [],
        IsSuccess: false,
        statuCode: 0,
        message: '',
    };

    public account: AccountLoginRequest = {
        password: '',
        email: '',
    };

    public isLogged: boolean = false;

    ngOnInit(): void {
        this.getTasks();
    }

    public getTasks() {
        const tasks = this.taskService
            .PostTask(this.account)
            .subscribe(task => {
                this.listTasks = task;
                if (this.listTasks.IsSuccess) {
                    this.isLogged = true;
                }
            });
        return tasks;
    }

    public DeleteTask(id: number) {
        return this.taskService.DeleteTask(id).subscribe(() => {
            this.getTasks();
        });
    }
}

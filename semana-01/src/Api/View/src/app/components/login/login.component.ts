import { Component, Input, inject, input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TasksService } from '../../services/tasks.service';
import { Tasks } from '../../models/todo';
import { AccountLoginRequest } from '../../models/AccountLoginRequest';
import { ApiResponse } from '../../models/ApiResponse';
import { TodoComponent } from '../todo/todo.component';
import { lastValueFrom } from 'rxjs';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [FormsModule, TodoComponent],
    templateUrl: './login.component.html',
})
export class LoginComponent {
    listTasks: ApiResponse<Array<Tasks>> = {
        data: [],
        IsSuccess: false,
        statuCode: 0,
        message: '',
    };

    isLogged = false;

    account: AccountLoginRequest = {
        password: '',
        email: '',
    };

    private taskService = inject(TasksService);

    async Login() {
        const tasks = this.taskService.PostTask(this.account);

        this.listTasks = await lastValueFrom(tasks);

        if (this.listTasks.data.length > 0) {
            this.isLogged = true;
        }

        return tasks;
    }
}

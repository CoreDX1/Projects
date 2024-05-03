import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TasksService } from '../../services/tasks.service';
import { AccountLoginRequest } from '../../models/AccountLoginRequest';
import { ApiResponse, Data } from '../../models/ApiResponse';
import { TodoComponent } from '../todo/todo.component';
import { lastValueFrom } from 'rxjs';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [FormsModule, TodoComponent],
    templateUrl: './login.component.html',
})
export class LoginComponent {
    listTasks: ApiResponse<Data> = {
        data: {
            lists: [],
            user: {
                createAt: '',
                email: '',
                password: '',
                userId: 0,
                userName: '',
            },
        },
        meta: {
            message: '',
            statusCode: 0,
        },
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

        if (this.listTasks.meta.statusCode == 200) {
            this.isLogged = true;
        }

        return tasks;
    }
}

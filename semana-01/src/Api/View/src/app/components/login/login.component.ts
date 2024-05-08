import { Component } from '@angular/core';
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
    templateUrl: './login.component.html'
})
export class LoginComponent {
    listTasks: ApiResponse<Data> = {
        meta: {
            statusCode: 0,
            message: ''
        },
        data: {
            user: {
                createAt: '',
                email: '',
                password: '',
                userId: 0,
                userName: ''
            },
            lists: []
        },
        errors: {
            Password: [],
            Email: []
        }
    };

    isLoggedIn = false;

    loginCredentials: AccountLoginRequest = {
        password: '',
        email: ''
    };

    private readonly taskService: TasksService;

    constructor(taskService: TasksService) {
        this.taskService = taskService;
    }

    public async AttemptLogin(): Promise<void> {
        try {
            const loginResponse = await lastValueFrom(this.taskService.PostTask(this.loginCredentials));
            console.log(loginResponse);
            this.listTasks = loginResponse;
            // if (loginResponse.meta.statusCode != HttpStatusCode.Unauthorized) {
            //     this.isLoggedIn = true;
            // }

            setInterval(() => {
                this.listTasks.errors = {
                    Email: [],
                    Password: []
                };
            }, 5000);
        } catch (error) {
            console.log(error);
        }
    }
}

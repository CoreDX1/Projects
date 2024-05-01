import { Component, Input, inject, input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TasksService } from '../../services/tasks.service';
import { Tasks } from '../../models/todo';
import { AccountLoginRequest } from '../../models/AccountLoginRequest';
import { ApiResponse } from '../../models/ApiResponse';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [FormsModule],
    templateUrl: './login.component.html',
})
export class LoginComponent {
    @Input() accountHijo = {
        email: '',
        password: '',
    };

    private taskService = inject(TasksService);

    public listTasks: ApiResponse<Array<Tasks>> = {
        data: [],
        IsSuccess: false,
        statuCode: 0,
        message: '',
    };

    public getTasks(accountLogin: AccountLoginRequest) {
        const tasks = this.taskService
            .PostTask(accountLogin)
            .subscribe(task => {
                this.listTasks = task;
            });

        console.log(this.listTasks);
        return tasks;
    }
}

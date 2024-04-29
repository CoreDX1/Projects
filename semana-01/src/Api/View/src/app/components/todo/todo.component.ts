import { Component, OnInit, inject } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { AccountLoginRequest } from '../../models/AccountLoginRequest';
import { Tasks } from '../../models/todo';

@Component({
    selector: 'app-todo',
    standalone: true,
    imports: [],
    templateUrl: './todo.component.html',
    styleUrl: './todo.component.css',
})
export class TodoComponent implements OnInit {
    private taskService = inject(TasksService);

    public listTasks: Tasks[] = [];

    public account: AccountLoginRequest = {
        password: 'password123',
        email: 'johndoe@example.com',
    };

    ngOnInit(): void {
        this.getTasks();
    }

    public getTasks() {
        let tasks = this.taskService.PostTask(this.account).subscribe(task => {
            this.listTasks = task;
        });
        return tasks;
    }
}

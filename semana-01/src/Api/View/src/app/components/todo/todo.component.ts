import { Component, OnInit, inject } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { AccountLoginRequest } from '../../models/AccountLoginRequest';

@Component({
    selector: 'app-todo',
    standalone: true,
    imports: [],
    templateUrl: './todo.component.html',
    styleUrl: './todo.component.css',
})
export class TodoComponent implements OnInit {
    private taskService = inject(TasksService);

    public account: AccountLoginRequest = {
        password: 'password123',
        email: 'johndoe@example.com',
    };

    ngOnInit(): void {
        this.taskService.PostTask(this.account).subscribe(task => {
            console.log(task);
        });
    }
}

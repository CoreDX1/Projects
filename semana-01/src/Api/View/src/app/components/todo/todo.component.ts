import { Component, Input, inject } from '@angular/core';
import { ApiResponse, Data } from '../../models/ApiResponse';
import { TasksService } from '../../services/tasks.service';

@Component({
    selector: 'app-todo',
    standalone: true,
    imports: [],
    templateUrl: './todo.component.html',
})
export class TodoComponent {
    private taskService = inject(TasksService);

    @Input() public listTasks: ApiResponse<Data> = {
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

    DeleteTaskId(id: number) {
        this.taskService.DeleteTask(id).subscribe();

        this.listTasks.data.lists = this.listTasks.data.lists.filter(
            item => item.id !== id
        );

        return this.listTasks;
    }
}

import { Component, Input } from '@angular/core';
import { ApiResult, Data } from '../../models/ApiResult';
import { TasksService } from '../../services/tasks.service';

@Component({
    selector: 'app-todo',
    standalone: true,
    imports: [],
    templateUrl: './todo.component.html'
})
export class TodoComponent {
    @Input() public listTasks: ApiResult<Data> | null = null;

    private readonly taskService: TasksService;

    constructor(taskService: TasksService) {
        this.taskService = taskService;
    }

    async DeleteTaskId(id: number) {
        try {
            await this.taskService.DeleteTask(id);

            if (!this.listTasks) return;

            this.listTasks.data.lists = this.listTasks.data.lists.filter((item) => item.id !== id);
            return this.listTasks;
        } catch (error) {
            console.log(error);
            return error;
        }
    }
}

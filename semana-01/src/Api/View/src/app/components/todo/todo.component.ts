import { Component, Input, OnInit, inject } from '@angular/core';
import { Tasks } from '../../models/todo';
import { ApiResponse } from '../../models/ApiResponse';

@Component({
    selector: 'app-todo',
    standalone: true,
    imports: [],
    templateUrl: './todo.component.html',
})
export class TodoComponent {
    @Input() public listTasks: ApiResponse<Array<Tasks>> = {
        data: [],
        IsSuccess: false,
        statuCode: 0,
        message: '',
    };
}

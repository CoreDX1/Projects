import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';

export const routes: Routes = [
    {
        path: 'todo',
        component: LoginComponent,
    },
    {
        path: '**',
        pathMatch: 'full',
        redirectTo: 'todo',
    },
];

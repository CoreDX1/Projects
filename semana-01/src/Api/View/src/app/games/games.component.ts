import { Component, Input } from '@angular/core';

interface Game {
    id: number;
    name: string;
}

@Component({
    selector: 'app-games',
    standalone: true,
    imports: [],
    templateUrl: './games.component.html',
    styleUrl: './games.component.css',
})
export class GamesComponent {
    @Input() username: string = '';

    public games: Game[] = [
        {
            id: 1,
            name: 'Dota 2',
        },
        {
            id: 2,
            name: 'League of Legends',
        },
        {
            id: 3,
            name: 'Metal Gear Solid V the Phantom Pain',
        },
        {
            id: 4,
            name: 'Elden Ring',
        },
        {
            id: 5,
            name: 'Red Dead Redemption 2',
        },
    ];
}

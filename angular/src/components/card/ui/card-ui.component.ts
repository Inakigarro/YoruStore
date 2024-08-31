import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-card-ui',
    templateUrl: './card-ui.component.html',
    styleUrl: './card-ui.component.scss'
})
export class CardUiComponent {
    @Input()
    public id: string;

    @Input()
    public titulo: string;

    @Input()
    public descripcion: string;

    @Input()
    public precio: number;
}
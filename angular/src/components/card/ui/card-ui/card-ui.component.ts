import { Component, EventEmitter, Input, Output, output } from '@angular/core';
import { Button } from '@components/models';
import { DetailsButtonClicked, ShoppingCartButtonClicked } from '../../state/card.actions';
import { Action } from '@ngrx/store';
@Component({
    selector: 'app-card-ui',
    templateUrl: './card-ui.component.html',
    styleUrl: './card-ui.component.scss'
})
export class CardUiComponent {
    @Input()
    public id: string = '';

    @Input()
    public titulo: string;

    @Input()
    public descripcion: string;

    @Input()
    public precio: number;

    @Output()
    public actionEmitter = new EventEmitter<Action>();

    public shoppingCartButton: Button = {
        type: 'raised',
        label: 'Agregar al carrito',
        icon: 'shopping_cart',
        action: ShoppingCartButtonClicked({itemId: this.id})
    }

    public detailsButton: Button = {
        type: 'basic',
        label: 'Detalles',
        icon: '',
        action: DetailsButtonClicked({itemId: this.id})
    }
}
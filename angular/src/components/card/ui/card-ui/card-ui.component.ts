import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Button } from '@components/models';
import { DetailsButtonClicked, ShoppingCartButtonClicked } from '../../state/card.actions';
import { Action } from '@ngrx/store';

@Component({
    selector: 'app-card-ui',
    templateUrl: './card-ui.component.html',
    styleUrl: './card-ui.component.scss'
})
export class CardUiComponent implements OnInit {
    @Input()
    public id: string;

    @Input()
    public titulo: string;

    @Input()
    public descripcion: string;

    @Input()
    public precio: number;

    @Output()
    public actionEmitter = new EventEmitter<Action>();

    public shoppingCartButton: Button;
    public detailsButton: Button;

    public ngOnInit(): void {
        this.shoppingCartButton = {
            type: 'raised',
            label: 'Agregar al carrito',
            icon: 'shopping_cart',
            action: ShoppingCartButtonClicked({itemId: this.id})
        };

        this.detailsButton = {
            type: 'basic',
            label: 'Detalles',
            icon: '',
            action: DetailsButtonClicked({itemId: this.id})
        }
    }
}
import { Component, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { Observable } from "rxjs";
import { selectMontoTotal } from "../state/shopping-cart.selector";

@Component({
    selector: 'shopping-cart-ui',
    templateUrl: 'shopping-cart-ui.component.html',
    styleUrl: 'shopping-cart-ui.component.scss',
})
export class ShoppingCartUiComponent implements OnInit{
    public montoTotal: Observable<number>;

    constructor(private store: Store){}
    
    public ngOnInit(): void {
        this.montoTotal = this.store.select(selectMontoTotal);
    }
}
import { Component, Input, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { Observable } from "rxjs";
import { selectMontoTotal } from "../state/shopping-cart.selectors";
import { ShoppingCartService } from "../shopping-cart.service";
import { Item } from "@root/components/models";

@Component({
	selector: "shopping-cart-ui",
	templateUrl: "shopping-cart-ui.component.html",
	styleUrl: "shopping-cart-ui.component.scss",
})
export class ShoppingCartUiComponent {
	@Input()
	public items: Item[];
	@Input()
	public montoTotal: number;
}

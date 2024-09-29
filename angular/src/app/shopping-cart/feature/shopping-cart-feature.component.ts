import { Component, OnInit } from "@angular/core";
import { ShoppingCartService } from "../shopping-cart.service";
import { Observable } from "rxjs";
import { Button, Item } from "@root/components/models";
import {
	BuyButtonClicked,
	CloseButtonClicked,
	EmptyButtonClicked,
} from "../state/shopping-cart.actions";

@Component({
	selector: "shopping-cart-feature",
	templateUrl: "shopping-cart-feature.component.html",
	styleUrl: "shopping-cart-feature.component.scss",
})
export class ShoppingCartFeatureComponent {
	public items$ = this.service.items$;
	public montoTotal$ = this.service.montonTotal$;
	public closeButton: Button = {
		type: "icon",
		label: "",
		icon: "close",
		action: CloseButtonClicked(),
	};
	public pagarButton: Button = {
		type: "raised",
		label: "Pagar",
		icon: "shopping_cart_checkout",
		action: BuyButtonClicked(),
	};
	public vaciarButton: Button = {
		type: "flat",
		label: "Vaciar",
		icon: "shopping_cart_off",
		action: EmptyButtonClicked(),
	};

	constructor(private service: ShoppingCartService) {}
}

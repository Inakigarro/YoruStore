import { Component, OnInit } from "@angular/core";
import { ShoppingCartService } from "../shopping-cart.service";
import { Observable } from "rxjs";
import { Item } from "@root/components/models";

@Component({
	selector: "shopping-cart-feature",
	templateUrl: "shopping-cart-feature.component.html",
})
export class ShoppingCartFeatureComponent {
	public items$ = this.service.items$;

	constructor(private service: ShoppingCartService) {}
}

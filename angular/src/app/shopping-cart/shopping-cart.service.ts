import { Injectable, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { Item } from "@root/components/models";
import { filter, map, Observable } from "rxjs";
import { selectAllItems } from "./state/shopping-cart.selectors";

@Injectable({
	providedIn: "root",
})
export class ShoppingCartService {
	public items$: Observable<Item[]> = this.store.select(selectAllItems);
	constructor(private store: Store) {}
}

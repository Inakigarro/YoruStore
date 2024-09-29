import { Injectable, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { Item } from "@root/components/models";
import { Observable } from "rxjs";
import {
	selectAllItems,
	selectMontoTotal,
} from "./state/shopping-cart.selectors";

@Injectable({
	providedIn: "root",
})
export class ShoppingCartService {
	public items$: Observable<Item[]> = this.store.select(selectAllItems);
	public montonTotal$: Observable<number> = this.store.select(selectMontoTotal);
	constructor(private store: Store) {}
}

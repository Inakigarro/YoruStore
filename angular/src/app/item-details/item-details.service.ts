import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import { Item } from "@root/components/models";
import { Observable } from "rxjs";
import { getCurrentItem } from "../state/app.selectors";

@Injectable({
	providedIn: "root",
})
export class ItemDetailsService {
	public currentItem$: Observable<Item | undefined>;

	constructor(private store: Store) {
		this.currentItem$ = this.store.select(getCurrentItem);
	}
}

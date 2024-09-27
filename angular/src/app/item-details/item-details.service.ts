import { Injectable } from "@angular/core";
import { Action, Store } from "@ngrx/store";
import { Item } from "@root/components/models";
import { filter, Observable, take } from "rxjs";
import { NavigationService } from "../navigation.service";
import { WebApiService } from "../web-api.service";
import { getItem, getItemLoaded } from "./state/item-details.selectors";
import { RouterService } from "../router/router.service";

export const DETAILS_CARD_TOOLBAR = "details-toolbar";

@Injectable({
	providedIn: "root",
})
export class ItemDetailsService {
	public currentItem$: Observable<Item | undefined>;
	public loaded$: Observable<boolean>;

	constructor(
		private store: Store,
		private navigationService: NavigationService,
		private routerService: RouterService,
		private webApi: WebApiService
	) {
		this.currentItem$ = this.store.select(getItem);
		this.loaded$ = this.store.select(getItemLoaded);
	}

	public dispatch(action: Action) {
		this.store.dispatch(action);
	}

	public navigate(url: string[], isRelative: boolean = false) {
		this.navigationService.navigate(url, isRelative);
	}

	public navigateToRoot() {
		this.routerService.routerUrl$.pipe(take(1)).subscribe((url) => {
			let categoria = url.split("/")[1];
			this.navigate([categoria]);
		});
	}

	public ObtenerItemPorId(itemId: string) {
		return this.webApi.obtenerItemPorId(itemId).pipe(filter((x) => !!x));
	}
}

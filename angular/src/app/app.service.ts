import { Injectable } from "@angular/core";
import { Action, Store } from "@ngrx/store";
import { selectAllToolbars } from "@root/components/toolbar/state/toolbar.selectors";
import { filter, map, Observable } from "rxjs";
import {
	getCategories,
	getCurrentUserProfile,
	getLoading,
	getMenuOpened,
	getShoppingCartCount,
	getShoppingCartOpened,
} from "./state/app.selectors";
import { WebApiService } from "./web-api.service";
import { UserProfile } from "./identity/models";
import { Item } from "@root/components/models";
import { AuthService } from "./auth/auth.service";
import { NavigationService } from "./navigation.service";

@Injectable({
	providedIn: "root",
})
export class AppService {
	public isMenuOpened$: Observable<boolean>;
	public isShoppingCartOpened$: Observable<boolean>;
	public currentUserProfile$: Observable<UserProfile | undefined>;
	public currentItem$: Observable<Item | undefined>;
	public shoppingCartCount$: Observable<number>;
	public loading$: Observable<boolean>;

	constructor(
		private store: Store,
		private webApi: WebApiService,
		private navigationService: NavigationService
	) {
		this.isMenuOpened$ = this.store.select(getMenuOpened);
		this.isShoppingCartOpened$ = this.store.select(getShoppingCartOpened);
		this.currentUserProfile$ = this.store.select(getCurrentUserProfile);
		this.shoppingCartCount$ = this.store.select(getShoppingCartCount);
		this.loading$ = this.store.select(getLoading);
	}

	public dispatch(action: Action) {
		this.store.dispatch(action);
	}

	public navigate(url: string[], relative: boolean) {
		this.navigationService.navigate(url, relative);
	}

	public getToolbarById(id: string) {
		return this.store
			.select(selectAllToolbars)
			.pipe(map((toolbars) => toolbars.find((t) => t.id == id)));
	}

	public obtenerCategorias() {
		return this.webApi.obtenerCategorias();
	}

	public obtenerCategoriaPorId(categoryId: string) {
		return this.store.select(getCategories).pipe(
			filter((categories) => categories.length > 0),
			map((categories) => categories.find((c) => c.id == categoryId))
		);
	}
}

import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { ShoppingCartService } from "../shopping-cart.service";
import { ShoppingCartButtonClicked } from "@components/card/state/card.actions";
import {
	filter,
	map,
	withLatestFrom,
} from "rxjs/operators";
import { ShoppingCartActions } from "./shopping-cart.actions";
import { Item } from "@components/models";
import { ItemDetailsActions } from "@root/app/item-details/state/item-details.actions";
import { AuthService } from "@root/app/auth/auth.service";
import { AuthActions } from "@root/app/auth/state/auth.actions";
import { Store } from "@ngrx/store";
import { selectAllItems, selectMontoTotal } from "./shopping-cart.selectors";

@Injectable()
export class ShoppingCartEffects {
	public upsertItem$ = createEffect(() =>
		this.actions.pipe(
			ofType(
				ShoppingCartButtonClicked,
				ItemDetailsActions.addShoppingCartButtonClicked
			),
			withLatestFrom(this.service.items$),
			map(([action, items]) => {
				let item = items.find((x) => x.id == action.item.id);
				if (item) {
					let updatedItem: Item = {
						id: item.id,
						titulo: item.titulo,
						descripcion: item.descripcion,
						cantidad: item.cantidad ? item.cantidad + 1 : 1,
						precio: item.precio,
					};
					return ShoppingCartActions.itemAdded({ item: updatedItem });
				}
				return ShoppingCartActions.itemAdded({
					item: {
						...action.item,
						cantidad: 1,
					},
				});
			})
		)
	);

	public buyButtonClicked$ = createEffect(() =>
		this.actions.pipe(
			ofType(ShoppingCartActions.buyButtonClicked),
			map(() => {
				let checkoutItems: Item[] = [];
				let checkoutMonto: number = 0;
				let items$ = this.store
					.select(selectAllItems)
					.subscribe((items) => (checkoutItems = items));
				let monto$ = this.store
					.select(selectMontoTotal)
					.subscribe((monto) => (checkoutMonto = monto));
				items$.unsubscribe();
				monto$.unsubscribe();
				if (checkoutItems.length > 0) {
					return ShoppingCartActions.checkOutRequested({
						items: checkoutItems,
						montoTotal: checkoutMonto,
					});
				}
				return ShoppingCartActions.checkOutEmpty();
			})
		)
	);

	public checkOutItems$ = createEffect(() =>
		this.actions.pipe(
			ofType(ShoppingCartActions.checkOutRequested),
			withLatestFrom(this.authService.isLoggedIn$),
			filter(([action, _]) => action.items.length > 0),
			map(([action, auth]) => {
				if (!auth) {
					return AuthActions.userNotLoggedIn();
				} else {
					return ShoppingCartActions.checkOutSucceeded({
						items: action.items,
						montoTotal: action.montoTotal,
					});
				}
			})
		)
	);

	constructor(
		private actions: Actions,
		private store: Store,
		private readonly service: ShoppingCartService,
		private readonly authService: AuthService
	) {}
}

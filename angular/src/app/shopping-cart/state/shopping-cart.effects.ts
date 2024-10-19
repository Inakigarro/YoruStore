import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { ShoppingCartService } from "../shopping-cart.service";
import { ShoppingCartButtonClicked } from "@components/card/state/card.actions";
import {
	catchError,
	filter,
	map,
	switchMap,
	take,
	withLatestFrom,
} from "rxjs/operators";
import {
	BuyButtonClicked,
	BuyButtonWithItemsAndTotal,
	CheckOutItems,
	ItemAddedToShoppingCart,
} from "./shopping-cart.actions";
import { Item } from "@components/models";
import { ItemDetailsActions } from "@root/app/item-details/state/item-details.actions";
import { AuthService } from "@root/app/auth/auth.service";
import { AuthActions } from "@root/app/auth/state/auth.actions";
import { itemDetailsReducer } from "@root/app/item-details/state/item-details.reducer";

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
					return ItemAddedToShoppingCart({ item: updatedItem });
				}
				return ItemAddedToShoppingCart({
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
			ofType(BuyButtonClicked),
			switchMap(() => this.service.items$),
			withLatestFrom(this.service.montonTotal$),
			filter(([items, monto]) => !!items),
			map(([items, monto]) =>
				BuyButtonWithItemsAndTotal({
					items: items,
					montoTotal: monto,
				})
			)
		)
	);

	public checkOutItems$ = createEffect(() =>
		this.actions.pipe(
			ofType(BuyButtonWithItemsAndTotal),
			withLatestFrom(this.authService.isLoggedIn$),
			filter(([action, auth]) => action.items.length > 0),
			map(([action, auth]) => {
				if (!auth) {
					return AuthActions.userNotLoggedIn();
				} else {
					return CheckOutItems({
						items: action.items,
						montoTotal: action.montoTotal,
					});
				}
			})
		)
	);
	constructor(
		private actions: Actions,
		private readonly service: ShoppingCartService,
		private readonly authService: AuthService
	) {}
}

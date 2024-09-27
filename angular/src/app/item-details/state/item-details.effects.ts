import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { ItemDetailsActions } from "./item-details.actions";
import { filter, map, switchMap, take, tap } from "rxjs";
import { RouterService } from "@root/app/router/router.service";
import { ItemDetailsService } from "../item-details.service";
import { routerNavigatedAction } from "@ngrx/router-store";

@Injectable()
export class ItemDetailsEffects {
	public loadCurrentItemFromNavigation$ = createEffect(() =>
		this.actions.pipe(
			ofType(routerNavigatedAction),
			switchMap(() => this.routerService.routerParams$.pipe(take(1))),
			filter((params) => !!params["itemId"]),
			switchMap((params) => this.service.ObtenerItemPorId(params["itemId"])),
			filter((x) => !!x),
			map((item) => ItemDetailsActions.itemLoaded({ item }))
		)
	);

	public backButtonClicked$ = createEffect(
		() =>
			this.actions.pipe(
				ofType(ItemDetailsActions.backButtonClicked),
				tap(() => this.service.navigateToRoot())
			),
		{ dispatch: false }
	);
	constructor(
		private actions: Actions,
		private service: ItemDetailsService,
		private routerService: RouterService
	) {}
}

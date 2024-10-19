import { Injectable } from "@angular/core";
import { AppService } from "../app.service";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { InitApp, SecondaryToolbarActions } from "./app.actions";
import { fetch } from "@ngrx/router-store/data-persistence";
import { filter, map, switchMap, tap, withLatestFrom } from "rxjs";
import { Button } from "@root/components/models";
import { RegisterToolbar } from "@root/components/toolbar/state/toolbar.actions";
import { SECONDARY_TOOLBAR_ID } from "../app-constants";
import { RouterService } from "../router/router.service";

@Injectable()
export class AppEffects {
	public initApp$ = createEffect(() =>
		this.actions$.pipe(
			ofType(InitApp),
			fetch({
				run: () =>
					this.appService.obtenerCategorias().pipe(
						filter((categorias) => categorias.length > 0),
						map((categories) =>
							SecondaryToolbarActions.categoriesObtained({
								categories: categories,
							})
						)
					),
				onError: (_, error) =>
					SecondaryToolbarActions.categoriesObtained({
						categories: [],
						error: error,
					}),
			})
		)
	);

	public registerCategoriesToolbar$ = createEffect(() =>
		this.actions$.pipe(
			ofType(SecondaryToolbarActions.categoriesObtained),
			filter(({ categories }) => categories.length > 0),
			map(({ categories }) => {
				let categoriesButtons: Button[];
				categoriesButtons = categories.map((cat) => {
					return {
						type: "fab",
						label: cat.nombre,
						icon: "",
						action: SecondaryToolbarActions.categoryButtonClicked({
							categoriaId: cat.id,
						}),
					};
				});
				return RegisterToolbar({
					toolbar: {
						id: SECONDARY_TOOLBAR_ID,
						secondaryButton: categoriesButtons,
						toolbarConfig: {
							isSecondaryToolbar: true,
							isTitleSeparete: false,
						},
					},
				});
			})
		)
	);

	public setFirstAsCurrent$ = createEffect(() =>
		this.actions$.pipe(
			ofType(SecondaryToolbarActions.categoriesObtained),
			withLatestFrom(this.routerService.routerParams$),
			filter(([{ categories }, params]) => !!categories && !!params),
			map(([{ categories }, _]) =>
				SecondaryToolbarActions.categoryButtonClicked({
					categoriaId: categories[0].id,
				})
			)
		)
	);

	public navigateToCategory$ = createEffect(
		() =>
			this.actions$.pipe(
				ofType(SecondaryToolbarActions.categoryButtonClicked),
				switchMap(({ categoriaId }) =>
					this.appService.obtenerCategoriaPorId(categoriaId)
				),
				tap((cat) => this.appService.navigate([cat?.nombre!], false))
			),
		{ dispatch: false }
	);

	constructor(
		private actions$: Actions,
		private appService: AppService,
		private routerService: RouterService
	) {}
}

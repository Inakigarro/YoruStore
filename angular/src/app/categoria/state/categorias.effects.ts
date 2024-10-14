import { Injectable } from "@angular/core";
import { CategoriasService } from "../categorias.service";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { InitApp, SecondaryToolbarActions } from "@root/app/state/app.actions";
import { filter, map, tap } from "rxjs";
import { CategoriesActions } from "./categorias.actions";
import { fetch } from "@ngrx/router-store/data-persistence";
import { Button } from "@root/components/models";
import { RegisterToolbar } from "@root/components/toolbar/state/toolbar.actions";
import { SECONDARY_TOOLBAR_ID } from "@root/app/app-constants";
import { DetailsButtonClicked } from "@root/components/card/state/card.actions";

@Injectable()
export class CategoriesEffects {
	public initCategories$ = createEffect(() =>
		this.actions.pipe(
			ofType(CategoriesActions.initCategorias),
			fetch({
				run: () =>
					this.service.obtenerCategorias().pipe(
						filter((categories) => !!categories),
						map((categories) =>
							CategoriesActions.categoriesObtained({ categories })
						)
					),
				onError: (action, error) =>
					CategoriesActions.categoriesObtained({
						categories: [],
						error: error,
					}),
			})
		)
	);

	public CategoryButtonClicked$ = createEffect(() =>
		this.actions.pipe(
			ofType(SecondaryToolbarActions.categoryButtonClicked),
			fetch({
				run: (action) =>
					this.service.obtenerCategoria(action.categoriaId).pipe(
						filter((categorie) => !!categorie),
						tap((categorie) =>
							this.service.navigate([`${categorie.nombre.toLowerCase()}`])
						),
						map((categorie) =>
							CategoriesActions.categoriaCargada({
								categoria: categorie,
							})
						)
					),
				onError: (action, error) =>
					CategoriesActions.categoriaCargada({
						categoria: { id: "", nombre: "", items: [] },
						error: error,
					}),
			})
		)
	);

	public registerCategoriesToolbar$ = createEffect(() =>
		this.actions.pipe(
			ofType(CategoriesActions.categoriesObtained),
			filter(({ categories }) => !!categories),
			map(({ categories }) => {
				let secondaryButtons: Button[] = [];
				categories.forEach((cat) => {
					let button: Button = {
						type: "fab",
						label: cat.nombre,
						icon: "",
						action: SecondaryToolbarActions.categoryButtonClicked({
							categoriaId: cat.id,
						}),
					};
					secondaryButtons.push(button);
				});
				return RegisterToolbar({
					toolbar: {
						id: SECONDARY_TOOLBAR_ID,
						secondaryButton: secondaryButtons,
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
		this.actions.pipe(
			ofType(CategoriesActions.categoriesObtained),
			filter(({ categories }) => !!categories),
			map(({ categories }) =>
				SecondaryToolbarActions.categoryButtonClicked({
					categoriaId: categories[0].id,
				})
			)
		)
	);

	public itemDetailsButtonClicked$ = createEffect(
		() =>
			this.actions.pipe(
				ofType(DetailsButtonClicked),
				tap(({ itemId }) => this.service.navigate([`${itemId}`], true))
			),
		{ dispatch: false }
	);
	constructor(
		private readonly actions: Actions,
		private service: CategoriasService
	) {}
}

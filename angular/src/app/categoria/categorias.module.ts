import { NgModule } from "@angular/core";
import { CategoriasListComponent } from "./list/categorias-list.component";
import { CommonModule } from "@angular/common";
import { CategoriasRoutingModule } from "./categorias-routing.module";
import { AppListModule } from "@root/components/list/list.module";
import { CategoriasRoutingComponent } from "./categorias-routing.component";
import { StoreModule } from "@ngrx/store";
import {
	CATEGORIES_FEATURE_KEY,
	categoriesReducer,
} from "./state/categorias.reducer";
import { EffectsModule } from "@ngrx/effects";
import { CategoriesEffects } from "./state/categorias.effects";

@NgModule({
	declarations: [CategoriasRoutingComponent, CategoriasListComponent],
	imports: [
		CommonModule,
		CategoriasRoutingModule,
		AppListModule,
		StoreModule.forFeature(CATEGORIES_FEATURE_KEY, categoriesReducer),
		EffectsModule.forFeature(CategoriesEffects),
	],
	exports: [CategoriasListComponent],
})
export class CategoriasModule {}

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
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { KeyUpDirective } from "./key-up.directive";

@NgModule({
	declarations: [
		CategoriasRoutingComponent,
		CategoriasListComponent,
		KeyUpDirective,
	],
	imports: [
		CommonModule,
		CategoriasRoutingModule,
		AppListModule,
		MatCardModule,
		MatIconModule,
		MatInputModule,
		MatFormFieldModule,
		MatButtonModule,
		MatProgressSpinnerModule,
		StoreModule.forFeature(CATEGORIES_FEATURE_KEY, categoriesReducer),
		EffectsModule.forFeature(CategoriesEffects),
	],
	exports: [CategoriasListComponent],
})
export class CategoriasModule {}

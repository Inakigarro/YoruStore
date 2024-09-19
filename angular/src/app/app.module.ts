import { NgModule, isDevMode } from "@angular/core";
import { AppComponent } from "./app.component";
import { CommonModule } from "@angular/common";
import { provideAnimationsAsync } from "@angular/platform-browser/animations/async";
import { AppRoutingModule } from "./app-routing.module";
import { RouterOutlet } from "@angular/router";
import { BrowserModule } from "@angular/platform-browser";
import { StoreModule } from "@ngrx/store";
import { EffectsModule } from "@ngrx/effects";
import { StoreRouterConnectingModule } from "@ngrx/router-store";
import { AppToolbarModule } from "@root/components";
import { StoreDevtoolsModule } from "@ngrx/store-devtools";
import { appReducer } from "./state/app.reducer";
import { AppListModule } from "../components/list/list.module";
import { AppCardModule } from "../components/card/card.module";
import { provideHttpClient } from "@angular/common/http";
import { AppEffects } from "./state/app.effects";
import { MatSidenavModule } from "@angular/material/sidenav";
import { ShoppingCartModule } from "./shopping-cart/shopping-cart.module";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";

@NgModule({
	declarations: [AppComponent],
	imports: [
		CommonModule,
		BrowserModule,
		AppRoutingModule,
		RouterOutlet,
		AppCardModule,
		AppListModule,
		AppToolbarModule,
		ShoppingCartModule,
		MatSidenavModule,
		MatProgressSpinnerModule,
		StoreModule.forRoot({ "app-state": appReducer }, {}),
		EffectsModule.forRoot(AppEffects),
		StoreRouterConnectingModule.forRoot(),
		StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: !isDevMode() }),
	],
	bootstrap: [AppComponent],
	providers: [provideAnimationsAsync(), provideHttpClient()],
})
export class AppModule {}

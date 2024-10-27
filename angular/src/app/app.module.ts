import { NgModule, isDevMode } from "@angular/core";
import { AppComponent } from "./app.component";
import { CommonModule } from "@angular/common";
import { provideAnimationsAsync } from "@angular/platform-browser/animations/async";
import { AppRoutingModule } from "./app-routing.module";
import { RouterOutlet } from "@angular/router";
import { BrowserModule } from "@angular/platform-browser";
import { StoreModule } from "@ngrx/store";
import { EffectsModule } from "@ngrx/effects";
import { routerReducer, StoreRouterConnectingModule } from "@ngrx/router-store";
import { AppToolbarModule } from "@root/components";
import { StoreDevtoolsModule } from "@ngrx/store-devtools";
import { appReducer } from "./state/app.reducer";
import { AppListModule } from "../components/list/list.module";
import { AppCardModule } from "../components/card/card.module";
import { HTTP_INTERCEPTORS, provideHttpClient } from "@angular/common/http";
import { MatSidenavModule } from "@angular/material/sidenav";
import { ShoppingCartModule } from "./shopping-cart/shopping-cart.module";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { CategoriasModule } from "./categoria/categorias.module";
import { authReducer } from "./auth/state/auth.reducer";
import { JwtInterceptor } from "./auth/jwt.interceptor";
import { LoginModule } from "./auth/login/login.module";
import { AppEffects } from "./state/app.effects";
import { AuthEffects } from "./auth/state/auth.effects";

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
		LoginModule,
		StoreModule.forRoot(
			{ routerReducer, "app-state": appReducer, auth: authReducer },
			{}
		),
		EffectsModule.forRoot(AppEffects, AuthEffects),
		StoreRouterConnectingModule.forRoot({ stateKey: "router-reducer" }),
		StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: !isDevMode() }),
	],
	bootstrap: [AppComponent],
	providers: [
		provideAnimationsAsync(),
		provideHttpClient(),
		{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
	],
})
export class AppModule {}

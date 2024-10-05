import Header from "./header";
import MainContent from "./main-content/main-content";
import SideBar from "./sidebar/sidebar";

export default function Home() {
  return (
    <>
      <Header />
      <div className="w-dvw h-5/6 flex flex-row">
        <SideBar buttonLabels={["Medias", "Pantalones"]}/>
        <MainContent />
      </div>
    </>
  );
}

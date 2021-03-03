import React, {useRef} from "react";
import DemoNavbar from "../components/Navbars/DemoNavbar";
import SimpleFooter from "../components/Footers/SimpleFooter";

export default function MainLayout(props) {
    const {children} = props;
    const main = useRef("main");

    return(
      <>
          <DemoNavbar ref={main}/>
          <main>
              {children}
          </main>
          <SimpleFooter/>
      </>
    );
}

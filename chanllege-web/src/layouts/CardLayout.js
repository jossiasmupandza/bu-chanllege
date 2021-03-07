import React from "react";
import { Card, Col, Container, Row} from "reactstrap";
import MainLayout from "./MainLayout";

export default function CardLayout(props){
    const {children, title, headerTitle, headerDescription, card} = props;

    return (
        <MainLayout>
            <section className="section-profile-cover section-shaped my-0">
                {/* Circles background */}
                <div className="shape shape-style-1 shape-default alpha-4">
                    <span />
                    <span />
                    <span />
                    <span />
                    <span />
                    <span />
                    <span />
                </div>
                <Col md={12} className="justify-content-center mt-9">
                    {
                        headerTitle &&
                            <h1 className="display-3 text-center text-white">
                                {headerTitle}
                            </h1>
                    }

                    {
                        headerDescription &&
                            <p className="text-center text-white">
                                {headerDescription}
                            </p>
                    }
                </Col>

                {/* SVG separator */}
                <div className="separator separator-bottom separator-skew">
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        preserveAspectRatio="none"
                        version="1.1"
                        viewBox="0 0 2560 100"
                        x="0"
                        y="0"
                    >
                        <polygon
                            className="fill-white"
                            points="2560 0 2560 100 0 100"
                        />
                    </svg>
                </div>
            </section>

            {
                card ?
                    <section className="section">
                        <Container>
                            <Card className="card-profile shadow mt--300 px-5 py-4">
                                {
                                    title &&
                                    <Row className="justify-content-center">
                                        <h1>{title}</h1>
                                    </Row>
                                }
                                <Row>
                                    {children}
                                </Row>
                            </Card>
                        </Container>
                    </section>
                :
                    <section className="section">
                        <Container>
                            {children}
                        </Container>
                    </section>
            }
        </MainLayout>
    );
}

import React from 'react';
export default function WithLoading(WrappedComponent, loadingDelegate) {
    class LoadingComponent extends React.Component {
        constructor(props) {

            super(props);
        }

        componentDidMount() {
            loadingDelegate(this);
        }

        render() {
            if (this.props.isLoading === true) {
                return (
                    <div>Loading...</div>
                );
            } else {
                return <WrappedComponent {...this.props} />;
            }
        }
    }

    return LoadingComponent;
}
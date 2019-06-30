import React from 'react';
export default function WithLoading(WrappedComponent, loadingDelegate) {
    class LoadingComponent extends React.Component {
        constructor(props) {
            super(props);
            this.loadingDelegate = loadingDelegate;
        }

        componentWillMount() {
            loadingDelegate(this);
        }

        render() {
            if (this.props.isLoading === true) {
                return (
                    <div>
                        <div>Loading...</div>
                    </div>
                );
            } else {
                return <WrappedComponent {...this.props} />;
            }
        }
    }

    return LoadingComponent;
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebIDEIBiblio.WSShippingAll {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WSShippingAll.ShippingAllSoap")]
    public interface ShippingAllSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/calculateShippingFees", ReplyAction="*")]
        double calculateShippingFees(int numberBooks);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ShippingAllSoapChannel : WebIDEIBiblio.WSShippingAll.ShippingAllSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ShippingAllSoapClient : System.ServiceModel.ClientBase<WebIDEIBiblio.WSShippingAll.ShippingAllSoap>, WebIDEIBiblio.WSShippingAll.ShippingAllSoap {
        
        public ShippingAllSoapClient() {
        }
        
        public ShippingAllSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ShippingAllSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ShippingAllSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ShippingAllSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double calculateShippingFees(int numberBooks) {
            return base.Channel.calculateShippingFees(numberBooks);
        }
    }
}

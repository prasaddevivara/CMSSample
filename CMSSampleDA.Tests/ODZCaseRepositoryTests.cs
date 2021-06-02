using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMSSample.DomainModel;
using CMSSample.DomainModel.ViewModels;
using CMSSample.DA.Repository;
using Moq;
using CMSSample.DA;

namespace CMSSampleDA.Tests
{
    [TestClass]
    public class ODZCaseRepositoryTests
    {
        [TestMethod]
        public void Test_GetMockAllODZCases()
        {
            List<ODZCaseDisplayViewModel> usrs = new List<ODZCaseDisplayViewModel>()
            {
                new ODZCaseDisplayViewModel{ODZCaseID=1, ODZCaseReference=12345, IncidentTypeName="Accident", DZName="France", CaseCoverageAmount=5000, AssistedPerson="Prasad", CaseDescription="Accident initimation" },
                new ODZCaseDisplayViewModel{ODZCaseID=2, ODZCaseReference=54331, IncidentTypeName="Service", DZName="India", CaseCoverageAmount=6000, AssistedPerson="Pavan", CaseDescription="Service request" }
            };
            var mock = new Mock<IODZCaseRepository>();
            mock.Setup(slf => slf.GetODZCases()).Returns(usrs);

            Assert.IsNotNull(usrs);
        }


        [TestMethod]
        public void Test_GetMockODZCaseByID()
        {
            var mock = new Mock<IODZCaseRepository>();
            ODZCaseEditViewModel odzc = new ODZCaseEditViewModel()
            { ODZCaseID = 1, ODZCaseReference = 54331, IncidentTypeID = "2",  SelectedCountryofIncidentID=1, CaseCoverageAmount = 6000, AssistedPerson = "Pavan", CaseDescription = "Service request" };
            
            int odzcID = 1;
            mock.Setup(slf => slf.GetODZCaseByID(odzcID)).Returns(odzc);

            Assert.IsNotNull(odzc);
        }

        [TestMethod]
        public void Test_MockInsertODZCase()
        {
            var mock = new Mock<IODZCaseRepository>();
            ODZCase odzc = new ODZCase()
            {
                ODZCaseID = 11,
                ODZCaseReference = 12345,
                IncidentTypeID = 1,
                CountryofIncidentID = 1,
                CaseCoverageAmount = 50000,
                AssistedPerson = "Venkat",
                CaseDescription ="Assistance request"
            };
            mock.Setup(slf => slf.InsertODZCase(odzc));
        }

        [TestMethod]
        public void Test_MockUpdateODZCase()
        {
            var mock = new Mock<IODZCaseRepository>();
            ODZCase odzc = new ODZCase()
            {
                ODZCaseID = 11,
                ODZCaseReference = 12345,
                IncidentTypeID = 1,
                CountryofIncidentID = 1,
                CaseCoverageAmount = 50000,
                AssistedPerson = "Venkat",
                CaseDescription = "Assistance request"
            };
            mock.Setup(slf => slf.UpdateODZCase(odzc));
        }

        [TestMethod]
        public void Test_MockDeleteODZCase()
        {
            var mock = new Mock<IODZCaseRepository>();
            mock.Setup(slf => slf.Delete(4));
        }

        [TestMethod]
        public void Test_GetAllODZCases()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IODZCaseRepository odzcRepository = new ODZCaseRepository(cmssampledacontext);

            IEnumerable<ODZCaseDisplayViewModel> lstodzc = null;

            lstodzc = odzcRepository.GetODZCases();
            Assert.IsNotNull(lstodzc);
        }

        [TestMethod]
        public void Test_GetODZCaseByID()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IODZCaseRepository odzcRepository = new ODZCaseRepository(cmssampledacontext);

            int odzcID = 1;
            ODZCaseEditViewModel odzceditvm = new ODZCaseEditViewModel();

            odzceditvm = odzcRepository.GetODZCaseByID(odzcID);
            Assert.IsNotNull(odzceditvm);
        }

        [TestMethod]
        public void Test_InsertODZCase()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IODZCaseRepository odzcRepository = new ODZCaseRepository(cmssampledacontext);
                        
            ODZCase odzc = new ODZCase()
            {                
                ODZCaseReference = 65478,
                IncidentTypeID = 1,
                CountryofIncidentID = 1,
                CaseCoverageAmount = 55000,
                AssistedPerson = "Ravi",
                CaseDescription = "Assistance request"
            };
            odzcRepository.InsertODZCase(odzc);            
        }

        [TestMethod]
        public void Test_DeleteODZCase()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IODZCaseRepository odzcRepository = new ODZCaseRepository(cmssampledacontext);

            odzcRepository.Delete(10);
        }

        [TestMethod]
        public void Test_UpdateODZCase()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IODZCaseRepository odzcRepository = new ODZCaseRepository(cmssampledacontext);
                       
            ODZCase odzc = new ODZCase()
            {
                ODZCaseID = 11,
                ODZCaseReference = 12345,
                IncidentTypeID = 1,
                CountryofIncidentID = 1,
                CaseCoverageAmount = 66000,
                AssistedPerson = "Ramana",
                CaseDescription = "Accident intimation"
            };
            odzcRepository.UpdateODZCase(odzc);            
        }
    }
}
